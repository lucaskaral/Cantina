import pika

def callback(ch, method, properties, body):
    print(f" [x] Recebido: {body}")

connection = pika.BlockingConnection(pika.ConnectionParameters('localhost'))
channel = connection.channel()

channel.queue_declare(queue='CantinaMessagesQueue')

channel.basic_consume(queue='CantinaMessagesQueue', on_message_callback=callback, auto_ack=True)

print(' [*] Aguardando mensagens')
channel.start_consuming()