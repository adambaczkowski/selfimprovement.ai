+++
title = "1. Local development"
description = "Guide on how to set up local environment"
weight = 2
+++

### First step

### Seconds step

### C# 
Do uruchomienia projektu potrzebny jest .NET 8.0, Docker, PostgresSQL
Żeby uruchomić serwer API projektu na początku należy wejść w folder docker-compose
uruchomić w nim terminal a następnie komendą : 
```
docker compose up -d –build
``` 
zbudować obraz aplikacji.
Żeby uruchomić front-end należy wejść w folder Web i uruchomić terminal. W terminalu na
początku trzeba uruchomić instalację “npm install” a, następnie można uruchomić aplikacje
```
npm start
```
Baza jest na porcie 1433, Identity Api 8080 a Web na porcie 3000
