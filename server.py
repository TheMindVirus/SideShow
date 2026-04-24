#!/usr/bin/python3
import socket, ssl, http.server

# https://www.digitalocean.com/community/tutorials/openssl-essentials-working-with-ssl-certificates-private-keys-and-csrs
# sudo openssl req -newkey rsa:2048 -nodes -keyout domain.key -x509 -days 365 -out domain.crt

data = "00" * 512

class Handler(http.server.SimpleHTTPRequestHandler):
    def __init__(self, *args, **kwargs):
        super().__init__(*args, **kwargs)

    def do_GET(self):
        global data
        if self.headers["Read-Command"] == "1":
            resp = "HTTP/1.1 200 OK\r\n"
            resp += "Content-Length: " + str(len(data))
            resp += "\r\n"
            resp += "\r\n"
            resp += data
            resp += "\r\n"
            self.wfile.write(resp.encode())
        else:
            super().do_GET()

    def do_POST(self):
        global data
        clen = int(self.headers["Content-Length"])
        data = self.rfile.read(clen).decode()
        resp = "HTTP/1.1 200 OK\r\n"
        resp += "Content-Length: " + str(len(data))
        resp += "\r\n"
        resp += "\r\n"
        resp += data
        resp += "\r\n"
        self.wfile.write(resp.encode())

    def end_headers(self):
        self.send_header("Access-Control-Allow-Origin", "*")
        http.server.SimpleHTTPRequestHandler.end_headers(self)

ctx = ssl.SSLContext(ssl.PROTOCOL_TLS_SERVER)
ctx.load_cert_chain("domain.crt", "domain.key")
ctx.check_hostname = False
httpd = http.server.HTTPServer(("0.0.0.0", 443), Handler)
httpd.socket = ctx.wrap_socket(httpd.socket, server_hostname = "localhost")
print("Please open https://127.0.0.1/ in a compatible web browser.")
httpd.serve_forever()

