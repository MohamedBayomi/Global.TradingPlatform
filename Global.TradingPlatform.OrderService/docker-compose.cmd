docker build -t orderserviceimage .
docker run -d -p 4200:4200 --name orderservicecontainer1 orderserviceimage