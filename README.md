# PDF number recognizer - CLI tool
Features:
- lines counter
- language detector
- number recognizer
- export to Elasticsearch

## Elastic search configuration
https://www.elastic.co/guide/en/elasticsearch/reference/current/docker.html
```
docker run --name es01 --net elastic -p 9200:9200 -it -m 1GB docker.elastic.co/elasticsearch/elasticsearch:8.15.2
```
