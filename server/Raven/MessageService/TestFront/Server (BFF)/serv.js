const { Issuer } = require('openid-client');
const express = require('express');
const bodyParser = require('body-parser');
const { HubConnectionBuilder } = require('@microsoft/signalr');
const app = express();

process.env["NODE_TLS_REJECT_UNAUTHORIZED"] = 0;

const issuerUrl = 'https://localhost:5001/';
const clientId = 'web';
const clientSecret = 'secret';

app.use(bodyParser.json());

(async () => {
  const issuer = await Issuer.discover(issuerUrl);
  const client = new issuer.Client({
    client_id: clientId,
    client_secret: clientSecret,
  });

  app.post('/api/authenticate', async (req, res) => {
    const { username, password } = req.body;

    try {
      const tokenSet = await client.grant({
        grant_type: 'password',
        username,
        password,
        scope: 'openid profile message',
      });

      console.log(tokenSet);
      res.json(tokenSet);
    } catch (error) {
      console.error('Error during authentication:', error);
      res.status(401).send('Authentication failed');
    }
  });

  app.listen(3001, () => {
    console.log('Backend server listening on port 3001');
  });
})();
