# COMANDI CURL

Aprire il terminale con gitbash, scaricare chocolatey e poi fare choco install jq nel powershell come amministratore.

Gli attributi:
- -H indica che stiamo inviando i dati in formato JSON.
- -d contiene i dati, in questo caso l'email e la password dell'utente che vogliamo loggare.

```bash
TOKEN=$(curl -s -X POST "http://localhost:5036/api/Auth/login" \
-H "Content-Type: application/json" \
-d '{"email": "luigi@email.com", "password":"654321"}' | jq -r '.token')
```

controlla il token: 
```bash
echo $TOKEN
```

Leggi interessi
```bash
curl -X GET "http://localhost:5036/api/Interests" \
-H "Authorization: Bearer $TOKEN"
```

Crea Interesse
```bash
curl -X POST "http://localhost:5036/api/Interests" \
-H "Content-Type: application/json" \
-H "Authorization: Bearer $TOKEN" \
-d '{"nome":"Scacchi"}'
```

Modifica interesse
```bash
curl -X PUT "http://localhost:5036/api/Interests"/1 \
-H "Content-Type: application/json" \
-H "Authorization: Bearer $TOKEN" \
-d '{"nome":"DotNet"}'
```

Elimina interesse
```bash
curl -X DELETE "http://localhost:5036/api/Interests"/11 \
-H "Authorization: Bearer $TOKEN"
```

Crea Utente
```bash
curl -X POST "http://localhost:5036/api/Auth/register" \
-H "Content-Type: application/json" \
-d '{"email":"luigi@email.com","password":"654321","nomeCompleto":"luigi bruno","phoneNumber":"3496266961","dataDiNascita":"1998-03-05","preferiti":true}'
```

Modifica utente(non implementato, ma si potrebbe fare aggiungendo un endpoint PUT in AuthController)
```bash
curl -X PUT "http://localhost:5036/api/Auth/update" \
-H "Content-Type: application/json"  \
-H "Authorization: Bearer $TOKEN" \
-d '{"nomeCompleto":"Mario Rossi Updated","phoneNumber":"3346548732"}'
```

Modifica utente(non implementato, ma si potrebbe fare aggiungendo un endpoint PUT in AuthController)
```bash
curl -X PUT "http://localhost:5036/api/Auth/update" \
-H "Content-Type: application/json"  \
-H "Authorization: Bearer $TOKEN" \
-d '{"nomeCompleto":"luigi bruno","phoneNumber":"3496266961","preferiti":true}'
```

Elimina utente(non implementato, ma si potrebbe fare aggiungendo un endpoint DELETE in AuthController)
```bash
curl -X DELETE "http://localhost:5036/api/Auth/delete" \
-H "Authorization: Bearer $TOKEN"
```

Stampa utente aggiornato:
```bash
curl -X GET "http://localhost:5036/api/Auth/profile" \
-H "Authorization: Bearer $TOKEN"
```