
## Description

Un projet web utilisant l'architecture N-Tiers avec le langage C#.

## Dépendances

- [Docker](https://docs.docker.com/engine/install/ubuntu/)
- [Rider](https://www.jetbrains.com/fr-fr/rider/)

## NuGet Packages
- Microsoft.EntityFrameworkCore
- Microsoft.EntityFrameworkCore.Design
- Microsoft.EntityFrameworkCore.Tools
- Npgsql.EntityFrameworkCore.PostgreSQL
- Castle.Windsor


## Installation
# Base de données PostgreSQL + PGadmin

```dockerfile
services:
  pgadmin:
    image: dpage/pgadmin4
    environment:
      - PGADMIN_DEFAULT_EMAIL=name@example.com
      - PGADMIN_DEFAULT_PASSWORD=admin
    ports:
      - "5050:80"
    restart: always

  postgres:
    image: postgres
    volumes:
      - postgresqldata:/data/db
    environment:
      - POSTGRES_PASSWORD=postgres
    ports:
      - "5432:5432"
    restart: always

volumes:
  postgresqldata:
```
## Running the app

```bash
# development
$ docker compose up
```
## Configuration

```sql

CREATE DATABASE student
    WITH
    OWNER = postgres
    ENCODING = 'UTF8'
    LC_COLLATE = 'en_US.utf8'
    LC_CTYPE = 'en_US.utf8'
    LOCALE_PROVIDER = 'libc'
    TABLESPACE = pg_default
    CONNECTION LIMIT = -1
    IS_TEMPLATE = False;

CREATE SEQUENCE IF NOT EXISTS public.sequence_students
    INCREMENT 1
    START 1
    MINVALUE 1
    MAXVALUE 9223372036854775807
    CACHE 1;

CREATE TABLE IF NOT EXISTS public.student
(
    "Id" integer NOT NULL DEFAULT nextval('sequence_students'::regclass),
    "Nom" character varying(50) COLLATE pg_catalog."default" NOT NULL,
    "Prenom" character varying(50) COLLATE pg_catalog."default" NOT NULL,
    CONSTRAINT students_pkey PRIMARY KEY ("Id")
)
```
