import { writable } from 'svelte/store';

export const chatsStore = writable({});

export const selectedChatId = writable();

export const userId = writable();

export const isDrawerClosed = writable(true);

export const addChatMessage = (chatId, user, message) => {
	chatsStore.update(chats => {
		if (!chats[chatId]) {
			chats[chatId] = { messages: [] };
		}
		chats[chatId].messages.push({ user, message });
		return chats;
	});
};
