# Echo client program
import socket
from threading import Thread

def enviarMsg(cliente):
    while True:
        mensagem = input("Mensagem: ")
        cliente.sendall(bytes(mensagem, "utf-8"))

HOST = 'localhost'
PORT = 5800
with socket.socket(socket.AF_INET, socket.SOCK_STREAM) as s:
    s.connect((HOST, PORT))
    
    #t1 = Thread(target=enviarMsg, args=[s])
    #t1.start()        
    
    while True:
        data = s.recv(1024)
        print('Receive: ', repr(data))
