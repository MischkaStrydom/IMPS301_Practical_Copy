#stage 1
FROM node:latest as node
WORKDIR /app
COPY . .
RUN npm install
RUN npm run build --prod
#stage 2
FROM nginx:alpine
COPY --from=node /app/dist/frontend /usr/share/nginx/html

COPY default.conf /etc/nginx/conf.d/


EXPOSE 80
CMD ["nginx", "-g", "daemon off;"]
