Manual Steps
------------
1. Install Ingress Controller by using the following command (for docker destop):-
     kubectl apply -f https://raw.githubusercontent.com/kubernetes/ingress-nginx/controller-v1.12.0/deploy/static/provider/cloud/deploy.yaml
     List the services by => kubectl get all -n ingress-nginx
     
2. Create K8S secret for MS SQL Server username and password
    kubectl create secret generic mssql-secret --from-literal=SA_PASSWORD="P@ssword10!"