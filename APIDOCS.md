# Telegram-like App Microservices API Documentation

This document outlines the API endpoints for each microservice in the Telegram-like application.

---

## AuthN/AuthZ Service (`/auth/*`)
Handles authentication, registration, token generation, and OAuth.

### Endpoints
- `POST /auth/login`
  - **Description**: User login.
  - **Request Body**: 
    ```
    {
      "username": "string",
      "password": "string"
    }
    ```
  - **Response**: JWT access token, refresh token.

- `POST /auth/register`
  - **Description**: Register a new user.
  - **Request Body**: 
    ```
    {
      "username": "string",
      "password": "string",
      "email": "string"
    }
    ```
  - **Response**: Confirmation of registration, UUID generation.

- `POST /auth/refresh-token`
  - **Description**: Refresh the access token using a refresh token.
  - **Request Body**: 
    ```
    {
      "refreshToken": "string"
    }
    ```
  - **Response**: New access token.

- `POST /auth/logout`
  - **Description**: Log out and invalidate the refresh token.

---

## UserService (`/user/*`)
Manages user profiles, settings, and other user-related actions.

### Endpoints
- `GET /user/{uuid}/profile`
  - **Description**: Get user profile by UUID.
  - **Response**: 
    ```
    {
      "username": "string",
      "email": "string",
      "settings": {}
    }
    ```

- `PUT /user/{uuid}/profile`
  - **Description**: Update user profile settings.
  - **Request Body**: 
    ```
    {
      "username": "string",
      "settings": {}
    }
    ```

- `DELETE /user/{uuid}`
  - **Description**: Delete a user profile.

- `GET /user/{uuid}/settings`
  - **Description**: Retrieve user settings.

- `PUT /user/{uuid}/settings`
  - **Description**: Update user settings.

---

## ChatService (`/chat/*`)
Handles chat creation, membership, and metadata.

### Endpoints
- `POST /chat/new`
  - **Description**: Create a new chat.
  - **Request Body**: 
    ```
    {
      "members": ["uuid1", "uuid2"],
      "chatType": "dialog"
    }
    ```
  - **Response**: 
    ```
    {
      "chatId": "string"
    }
    ```

- `POST /chat/group`
  - **Description**: Create a new group chat.
  - **Request Body**: 
    ```
    {
      "groupName": "string",
      "members": ["uuid1", "uuid2", "uuid3"]
    }
    ```
  - **Response**: 
    ```
    {
      "chatId": "string",
      "groupName": "string",
      "members": ["uuid"]
    }
    ```

- `GET /chat/{chatId}`
  - **Description**: Get chat details by chat ID.
  
- `GET /chat/{chatId}/members`
  - **Description**: Get the members of a chat.

- `POST /chat/{chatId}/add-member`
  - **Description**: Add a member to an existing chat.
  
- `DELETE /chat/{chatId}/remove-member`
  - **Description**: Remove a member from the chat.

- `POST /chat/{chatId}/leave`
  - **Description**: Leave the chat.

---

## MessageService (`/message/*`)
Handles messages, including sending and receiving messages, and SignalR connections for real-time communication.

### Endpoints
- `POST /message/{chatId}/send`
  - **Description**: Send a message to a chat.
  - **Request Body**: 
    ```
    {
      "content": "string",
      "sender": "uuid",
      "type": "text/image/etc."
    }
    ```
  - **Response**: 
    ```
    {
      "messageId": "string",
      "timestamp": "string"
    }
    ```
  
- `GET /message/{chatId}`
  - **Description**: Retrieve messages from a chat.
  
- `DELETE /message/{chatId}/{messageId}`
  - **Description**: Delete a specific message.

---

## ManagementService (`/management/*`)
Coordinates cross-service tasks like user deletion, which affects both `AuthService` and `UserService`.

### Endpoints
- `DELETE /management/user/{uuid}`
  - **Description**: Delete a user from both `AuthService` and `UserService`.

- `GET /management/logs`
  - **Description**: Get a list of tasks or logs involving cross-service management (like deletions).

---

## TemporaryMsgService (`/temporary/*`)
Handles temporary, self-destructing messages stored in Redis with a specified lifetime.

### Endpoints
- `POST /temporary/{chatId}/send`
  - **Description**: Send a temporary message that will self-destruct after a specific time.
  - **Request Body**: 
    ```
    {
      "content": "string",
      "expiryTime": "timestamp"
    }
    ```
  
- `DELETE /temporary/{chatId}/{messageId}`
  - **Description**: Delete a temporary message manually before its expiry.

- `GET /temporary/{chatId}`
  - **Description**: Retrieve active temporary messages for a chat.

---

## MediaService (`/media/*`)
Handles media uploads and downloads, and file storage in a CDN.

### Endpoints
- `POST /media/upload`
  - **Description**: Upload media (images, videos, files).
  - **Request Body**: File data.
  - **Response**: 
    ```
    {
      "mediaUrl": "string"
    }
    ```

- `GET /media/{mediaId}/download`
  - **Description**: Download media by ID.

- `DELETE /media/{mediaId}`
  - **Description**: Delete a media file.

- `GET /media/{mediaId}/metadata`
  - **Description**: Retrieve metadata about a media file.

---

## RabbitMQ Events

### `AuthService` -> `UserService` Event
- **Event**: `UserRegistered`
- **Payload**: 
    ```
    {
      "uuid": "string",
      "email": "string",
      "createdAt": "timestamp"
    }
    ```

### `MessageService` -> `ChatService` Event
- **Event**: `MessageSent`
- **Payload**: 
    ```
    {
      "messageId": "string",
      "chatId": "string",
      "senderUuid": "string",
      "content": "string",
      "timestamp": "timestamp"
    }
    ```

### `ManagementService` Event
- **Event**: `UserDeleted`
- **Payload**:
    ```
    {
      "uuid": "string",
      "deletedAt": "timestamp"
    }
    ```
