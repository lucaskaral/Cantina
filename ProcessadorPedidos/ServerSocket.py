from threading import Thread
import socket

listaClientes = []

def enviarMsg(listaClientes, cliente):
    while True:
        mensagem = repr(cliente.recv(1024))
        for cli in listaClientes:
            cli.sendall(bytes(mensagem, "utf-8"))

HOST = ''
PORT = 5800

with socket.socket(socket.AF_INET, socket.SOCK_STREAM) as ss:
    ss.bind((HOST,PORT))
    ss.listen(1)
    while True:
        conn, addr = ss.accept()
        listaClientes.append(conn)
        threadEnviaMsg = Thread(target=enviarMsg, args=[listaClientes, conn])
        threadEnviaMsg.start()

