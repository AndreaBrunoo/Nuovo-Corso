
using Newtonsoft.Json;

/*
    Comando per la libreria Newtonsoft.Json
    dotnet add package Newtonsoft.Json
*/

// File json semplice 
{
"nome": "Partecipante 1",
"eta": 30,
"presente": true
}

// File json complesso 
{
"nome": "Partecipante 1",
"eta": 30,
"presente": true,
"interessi": ["programmazione", "musica", "sport"]
}

// File json con oggetto
{
"nome": "Partecipante 1",
"eta": 30,
"presente": true,
"interessi": ["programmazione", "musica", "sport"],
"indirizzo": {
    "via": "Via Roma",
    "citta": "Milano",
    "cap": "20100"
  }
}

// Elenco di oggetti json
[
  {
    "nome": "Partecipante 1",
    "eta": 30,
    "presente": true,
    "interessi": ["programmazione", "musica", "sport"],
    "indirizzo": {
        "via": "Via Roma",
        "citta": "Milano",
        "cap": "20100"
      }
  },
  {
    "nome": "Partecipante 2",
    "eta": 25,
    "presente": false,
    "interessi": ["cinema", "viaggi", "cucina"],
    "indirizzo": {
        "via": "Via Milano",
        "citta": "Roma",
        "cap": "00100"
      }
  }
]

