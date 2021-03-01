<#
*********************
*******Namespace********
*********************
#>
kubectl create namespace k8svolume

kubectl get ns
kubectl get all --namespace k8svolume

<#
*********************
*******Secret********
*********************
#>
kubectl apply -f ..\k8s\secrets.yaml
kubectl get secret --namespace k8svolume
kubectl get secret k8svolume-secret --namespace k8svolume -o jsonpath="{.data.App-Secret}"
kubectl describe secret k8svolume-secret --namespace k8svolume 
kubectl delete secret  k8svolume-secret --namespace k8svolume #

<#
*********************
*******Config Map****
*********************
#>
kubectl apply -f .\web\k8s\configmap.yaml
kubectl describe configmaps k8svolume-appsettings --namespace k8svolume 

<#
*********************
*******Deployment********
*********************
#>
kubectl delete -f ..\k8s\deployment.yaml ##
kubectl apply -f ..\k8s\deployment.yaml
kubectl get pod  --namespace k8svolume 
kubectl get deployment   --namespace k8svolume 

Kubectl describe nodes
kubectl describe deployment k8svolume-deployment  --namespace k8svolume

kubectl get pod  --namespace k8svolume 
kubectl describe pod k8svolume-deployment-59774b7c78-qx69n --namespace k8svolume  
kubectl delete pod k8svolumerun --namespace k8svolume  

kubectl exec -it k8svolume-deployment-59774b7c78-qx69n /bin/bash --namespace k8svolume

<#
*********************
*******Service********
*********************
#>
kubectl apply -f ..\k8s\service.yaml

kubectl get service --namespace k8svolume  -o wide

kubectl run nginxpod --image=nginx --namespace k8svolume
kubectl delete pod nginxpod --namespace k8svolume