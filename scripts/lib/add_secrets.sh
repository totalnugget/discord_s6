#!/usr/bin/env bash

ROOT_PATH=$( cd "$(dirname "${BASH_SOURCE[0]%/*}")" ; pwd )/..

ENV_FILE=$ROOT_PATH/.env

# Export the vars in .env into your shell:
if [[ "$OSTYPE" == *"darwin"* ]]; then
    export $(egrep -v '^#' $ENV_FILE | xargs)
else
    source $ENV_FILE
fi


# User
kubectl create secret generic -n core user-db-auth --from-literal=POSTGRES_USER=$USER_DB_USERNAME --from-literal=POSTGRES_PASSWORD=$USER_DB_PASSWORD

# Progress
#kubectl create secret generic -n core progress-db-auth --from-literal=POSTGRES_USER=$PROGRESS_DB_USERNAME --from-literal=POSTGRES_PASSWORD=$PROGRESS_DB_PASSWORD

# Keycloak
kubectl create secret generic -n core realm-secret --from-file=$ROOT_PATH/deployments/keycloak/data/realm.json
kubectl create secret generic -n core keycloak-auth --from-literal=username=$KEYCLOAK_USERNAME --from-literal=password=$KEYCLOAK_PASSWORD
kubectl create secret generic -n core keycloak-db-auth --from-literal=POSTGRES_USER=$KEYCLOAK_DB_USERNAME --from-literal=POSTGRES_PASSWORD=$KEYCLOAK_DB_PASSWORD

#Exergame
#kubectl create secret generic -n core exergame-db-auth --from-literal=POSTGRES_USER=$EXERGAME_DB_USERNAME --from-literal=POSTGRES_PASSWORD=$EXERGAME_DB_PASSWORD

# rabbitmq secrets
echo -n $(echo -n $RABBITMQ_PASSWORD | base64) > $ROOT_PATH/deployments/rabbitmq/.env.dev
kubectl create secret generic -n core rabbitmq-user --from-literal=rabbitmq-password=$RABBITMQ_PASSWORD
kubectl create secret generic -n core rabbitmq-erlang --from-literal=rabbitmq-erlang-cookie=$(head -c 48 /dev/random | base64)

# Organisation
#kubectl create secret generic -n core organisation-db-auth --from-literal=POSTGRES_USER=$ORGANISATION_DB_USERNAME --from-literal=POSTGRES_PASSWORD=$ORGANISATION_DB_PASSWORD
