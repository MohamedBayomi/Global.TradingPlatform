docker build -t orderupdaterimage .
docker run -d --name orderupdatercontainer1 orderupdaterimage