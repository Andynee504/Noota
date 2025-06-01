
# Projeto Noota

Este é um projeto em desenvolvimento no Unity. Aqui estão as instruções básicas para colaborar com o projeto via Git.

---

## Primeiros passos

**Observação:**  
Os comandos abaixo devem ser usados no **Prompt de Comando (CMD)** do Windows.

**Dica:** Para abrir o terminal no local certo, vá até a pasta desejada no Windows Explorer, segure `Shift`, clique com o botão direito e selecione **“Abrir janela de comando aqui”**.

Antes de clonar o projeto, escolha um local no seu computador onde deseja guardar os arquivos, por exemplo:

```
C:\Users\SeuUsuário\ProjetosUnity\
```

**Importante:** Evite nomes com acentos, espaços ou caracteres especiais nas pastas, pois isso pode causar problemas com o Unity ou scripts automatizados.

Dentro da pasta escolhida, no CMD, execute o seguinte comando:

```bash
git clone https://github.com/Andynee504/Noota.git
```

Isso criará uma nova pasta chamada `Noota` com todos os arquivos do projeto.  
Abra essa pasta no Unity (versão recomendada: **6000.1.3f1**) para começar a trabalhar.

---

## Acesso autorizado para colaborar

Este projeto funciona com um grupo pequeno de colaboradores. Se você recebeu acesso, poderá enviar alterações diretamente com `git push`.

Caso seja sua **primeira vez usando o Git** no computador, será necessário se autenticar com sua conta do GitHub ao tentar fazer `push`.  
Siga as instruções que aparecerem no terminal (ex: login via navegador).

---

## Mantendo seu repositório atualizado

Antes de começar a trabalhar, sempre sincronize com o repositório remoto:

```bash
git pull --rebase origin main
```

Isso evita conflitos e garante que você está trabalhando na versão mais recente do projeto.

---

## Salvando alterações

Após fazer modificações:

```bash
git add .
git commit -m "Descreva aqui a alteração feita"
git push
```

---

## Changelog automático

Este projeto inclui um arquivo `commit.bat`. Para registrar suas alterações:

1. Clique duas vezes em `commit.bat`  
2. Escreva o que foi alterado (use `*` para listas)  
3. Clique em OK – e pronto, o Git cuidará do resto!

---

## .gitignore e pasta Library

Este projeto já inclui um `.gitignore` configurado corretamente para Unity.

A pasta `Library/` não é enviada ao repositório e será automaticamente regenerada ao abrir o projeto.

---

## Recomendações

- Não use `git push --force` a menos que realmente necessário.
- Trabalhamos diretamente na branch `main`.
- Se tiver dúvidas ou sugestões, fale no canal do Discord/WhatsApp.
