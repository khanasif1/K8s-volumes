docker rm -f k8svolumerun
docker run -d -p 8080:80 -v $pwd\Settings\:/app/Settings --name k8svolumelocal k8s.volumes:0.1
docker ps -a
docker exec -it k8s.volumes /bin/bash

docker rm -f $(docker ps -a -q) -f