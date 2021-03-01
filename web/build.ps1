cd .\web\K8s.Volumes\
docker build --no-cache  -t k8s.volumes:0.1 .
docker images    
<#
docker rmi -f 79df6884f635
docker rmi -f $(docker images --filter=reference="khanasif1/k8s.volumes" -q)
docker rmi -f $(docker images --filter=reference="k8s.volumes" -q) 
#>
