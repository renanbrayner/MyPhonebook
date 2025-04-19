# MyPhonebook Frontend

Este diretório contém a aplicação frontend do **MyPhonebook**, construída com **Vue 3**, **Vite**, **TypeScript** e **PrimeVue**.

## Visão Geral

A aplicação permite visualizar, criar, editar e excluir contatos (nome, telefone e e-mail). Comunica-se com o backend via API REST.

## Estrutura do Projeto

```
frontend/
├── public/               # Arquivos estáticos
├── src/
│   ├── assets/           # Imagens e estilos globais
│   ├── components/       # Componentes Vue reutilizáveis
│   │   ├── BaseInput.vue # Wrapper genérico para inputs com FloatLabel e ícones
│   │   └── contacts/
│   │       ├── list/     # Lista de contatos
│   │       │   ├── ContactList.vue
│   │       │   └── ContactListItem.vue
│   │       └── form/     # Formulário de criação/edição
│   │           └── ContactForm.vue
│   ├── composables/      # Composables Vue (hooks)
│   │   └── useContacts.ts
│   ├── router/           # Configuração de rotas (vue-router)
│   │   └── index.ts
│   ├── services/         # Módulo de comunicação com API (axios)
│   │   └── contactsService.ts
│   ├── types/            # Definições de tipos TypeScript
│   │   └── contact.ts
│   ├── utils/            # Utilitários, como tratamento de erro
│   │   └── errorHandler.ts
│   ├── App.vue           # Componente raiz
│   ├── main.ts           # Entrada da aplicação
│   └── vite-env.d.ts     # Tipagens do Vite
├── .gitignore            # Arquivos ignorados pelo Git
├── package.json          # Dependências e scripts
├── pnpm-lock.yaml        # Lockfile do pnpm
└── tsconfig.json         # Configuração do TypeScript
```

## Tecnologias Principais

- **Vue 3** com Composition API e `<script setup>`
- **Vite** como bundler
- **TypeScript** para tipagem estática
- **PrimeVue** para componentes de interface (Panel, Button, Toast, ConfirmDialog, InputMask, etc.)
- **Tailwind CSS** para estilos utilitários
- **Axios** para chamadas HTTP
- **Zod** e **PrimeVue Forms** para validação de formulários

## Instalação e Execução

### Pré-requisitos
- Node.js (v16+)
- pnpm (recomendado) ou npm

### Instalando dependências

```bash
cd frontend
pnpm install
```

### Executando em modo de desenvolvimento

```bash
pnpm dev
```

Abrir `http://localhost:5173` no navegador.

### Gerando build de produção

```bash
pnpm build
```

O conteúdo ficará em `dist/`, pronto para servir por um servidor HTTP.

### Variáveis de Ambiente

- `VITE_API_URL`: URL base da API (ex: `http://localhost:5036`). Configurada em `.env` ou diretamente no `vite.config.ts`.

## Rotas

- **/**: Página inicial (lista de contatos + busca)
- **/contato**: Formulário de criação
- **/contato/:id**: Formulário de edição (carrega dados do contato)

## Gerenciamento de Estado

O estado de contatos é mantido via composable `useContacts`, que:

- Faz fetch inicial no `onMounted`
- Oferece funções `loadContacts`, `addContact`, `editContact`, `deleteContact`
- Realiza _optimistic updates_ em deleção

## Validação e Feedback

- **PrimeVue Forms** + **zodResolver**: validação declarativa de campos
- **Toast** para notificações de sucesso/erro
- **ConfirmDialog** e **ConfirmPopup** para confirmações de exclusão

## Componentes Reutilizáveis

- **BaseInput.vue**: encapsula `FloatLabel`, `IconField` e slot para input (text, mask)
- **ContactList.vue** / **ContactListItem.vue**: exibição de lista com painel expansível
- **ContactForm.vue**: formulário de criação/edição com validação e toast

## Testes de Integração

Scripts de teste end-to-end com **Cypress** (diretório `cypress/`):

- Criação de contato
- Busca e filtro
- Edição de contato
- Exclusão de contato

Executar:

```bash
pnpm cypress open
# ou headless
pnpm cypress run
```

## Decisões de Design

- **Separação de responsabilidades**: composables para lógica, serviços para chamadas HTTP, componentes puros para UI
- **Tipagem forte**: TypeScript + zod para garantir integridade de dados
- **UX**: feedback imediato via toast e confirmações antes de ações destrutivas
- **Performance**: Vite em modo dev e build otimizados

---
Este README fornece um guia completo para entender, executar e estender o frontend do MyPhonebook.
