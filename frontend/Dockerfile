# build dos assets estáticos
FROM node:18-alpine AS build
WORKDIR /app

# instala pnpm globalmente
RUN npm install -g pnpm

COPY package.json pnpm-lock.yaml ./
RUN pnpm install

COPY . .
RUN pnpm build

# etapa de serve estático
FROM nginx:stable-alpine AS runtime
COPY --from=build /app/dist /usr/share/nginx/html

# remove o banner do welcome do nginx
RUN rm /etc/nginx/conf.d/default.conf

# config simples: serve tudo de /usr/share/nginx/html
COPY <<EOF /etc/nginx/conf.d/app.conf
server {
    listen       8080;
    server_name  localhost;
    location / {
        root   /usr/share/nginx/html;
        index  index.html;
        try_files \$uri \$uri/ /index.html;
    }
}
EOF

EXPOSE 8080
CMD ["nginx", "-g", "daemon off;"]
