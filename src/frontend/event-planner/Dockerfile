FROM node:19-alpine 
EXPOSE 3000

WORKDIR /app
COPY . .

ARG REACT_APP_HOST_ADDRESS
ENV REACT_APP_HOST_ADDRESS $REACT_APP_HOST_ADDRESS

# Install dependencies and build app
# (npm ci makes sure the exact versions in the lockfile gets installed)
RUN npm ci 
RUN npm run build

# Start the app
CMD [ "npx", "serve", "build" ]