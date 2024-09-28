docker build -t streamerserviceimage .
docker run -d -p 5100:5100 --name streamerservicecontainer1 streamerserviceimage