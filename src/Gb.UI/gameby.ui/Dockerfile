# Stage 1: Build the React app
FROM node:16 AS build

# Set working directory
WORKDIR /app

# Copy package.json and package-lock.json to leverage Docker cache
COPY package.json package-lock.json ./

# Install dependencies
RUN npm install

# Copy the rest of the app's source code
COPY . .

# Build the React app
RUN npm run build

# Stage 2: Serve the app with Nginx
FROM nginx:alpine

# Copy built files to the Nginx HTML directory
COPY --from=build /app/build /usr/share/nginx/html

# Use the default Nginx configuration file
# Optionally, replace with a custom one if needed
# COPY nginx.conf /etc/nginx/nginx.conf

# Expose the port the app runs on
EXPOSE 80