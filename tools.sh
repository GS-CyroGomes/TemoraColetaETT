#!/usr/bin/env bash

BLUE="\033[1;34m"
YELLOW="\033[1;33m"
GREEN="\033[1;32m"
RED="\033[1;31m"
RESTORE="\033[0m"

showHelp() {
    echo -e "${BLUE}showHelp${RESTORE}                          ${YELLOW}Exibe a lista de comandos disponíveis.${RESTORE}"
    echo -e "${BLUE}installConfigEnvironment${RESTORE}          ${YELLOW}Instala e configura o ambiente.${RESTORE}"
}

installConfigEnvironment() {
    checkInstallDotNet
    checkInstallDocker
}

checkInstallDotNet() {
    echo -e "${BLUE}Verificando .NET 8 SDK...${RESTORE}"
    if command -v dotnet >/dev/null 2>&1; then
        if dotnet --list-sdks | grep -q '^8\.'; then
            echo -e "${GREEN}.NET 8 já está instalado.${RESTORE}"
        else
            echo -e "${YELLOW}Outra versão do .NET encontrada. Instalando .NET 8...${RESTORE}"
            sudo apt-get update
            sudo apt-get install -y dotnet-sdk-8.0
        fi
    else
        echo -e "${YELLOW}.NET não encontrado. Instalando .NET 8...${RESTORE}"
        sudo apt-get update
        sudo apt-get install -y dotnet-sdk-8.0
    fi
}

checkInstallDocker() {
    echo -e "${BLUE}Verificando Docker...${RESTORE}"
    if command -v docker >/dev/null 2>&1; then
        echo -e "${GREEN}Docker já está instalado.${RESTORE}"
    else
        echo -e "${YELLOW}Docker não encontrado. Instalando...${RESTORE}"
        sudo apt-get update
        sudo apt-get install -y docker.io
    fi

    # Garante que o serviço está rodando e habilitado
    if ! systemctl is-active --quiet docker; then
        echo -e "${YELLOW}Iniciando serviço Docker...${RESTORE}"
        sudo systemctl start docker
    fi
    if ! systemctl is-enabled --quiet docker; then
        echo -e "${YELLOW}Habilitando Docker na inicialização...${RESTORE}"
        sudo systemctl enable docker
    fi
}

unitTests() {
    dotnet restore
    dotnet build
    dotnet test
}

showHelp
