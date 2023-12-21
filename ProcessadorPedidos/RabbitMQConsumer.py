import pika
import json
import socket
from threading import Thread

HOST = 'localhost'
PORT = 5800

def callback(ch, method, properties, body):
    data = body.decode("utf-8")
    print(f" [x] Processando Pedido: {json.dumps(data)}") #print(f" [x] Recebido: {json.dumps( body )}")
    with socket.socket(socket.AF_INET, socket.SOCK_STREAM) as socketClient:
        socketClient.connect((HOST, PORT))
        socketClient.sendall(bytes(data, "utf-8"))
        #socketClient.close()
    

connection = pika.BlockingConnection(pika.ConnectionParameters('localhost'))
channel = connection.channel()

channel.queue_declare(queue='CantinaMessagesQueue')

channel.basic_consume(queue='CantinaMessagesQueue', on_message_callback=callback, auto_ack=True)

print(' [*] Aguardando mensagens')
channel.start_consuming()