FROM node:alpine as builder
WORKDIR /app
COPY package*.json ./
RUN npm install -g hugo
COPY ./Content .
RUN hugo
FROM nginx:alpine
COPY --from=builder /app/public /usr/share/nginx/html
EXPOSE 80
# Start Nginx
CMD ["nginx", "-g", "daemon off;"]
