TODO: Criar aqui a documentação do OrderExecutionWorker

docker exec -it redis-order-execution redis-cli

LPUSH commands:send-order '{"Account": "ACC-002", "OrderId":"ORD-002", "Symbol":"VALE3","Side":0, "Quantity":50, "Type": 1, "Price":68.10}'

LLEN commands:send-order