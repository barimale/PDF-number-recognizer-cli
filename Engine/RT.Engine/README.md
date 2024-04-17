# Elasticsearch - step by step
```
docker network create elastic
docker pull docker.elastic.co/elasticsearch/elasticsearch:8.13.2
docker run --name es01 --net elastic -p 9200:9200 -it -m 1GB docker.elastic.co/elasticsearch/elasticsearch:8.13.2

docker pull docker.elastic.co/kibana/kibana:8.13.2
docker run --name kib01 --net elastic -p 5601:5601 docker.elastic.co/kibana/kibana:8.13.2
```
