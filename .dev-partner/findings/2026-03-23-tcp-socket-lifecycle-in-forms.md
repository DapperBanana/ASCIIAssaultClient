---
title: TCP socket lifecycle tied to Form lifetime — watch for orphaned connections
date: 2026-03-23
tags: [networking, gotcha, c#]
type: gotcha
project: DapperBanana/ASCIIAssaultClient
---

Coupling socket state to MainForm's lifecycle creates a trap: if the form closes before explicit disconnect (e.g., force kill, unhandled exception), the socket won't cleanly shut down. The server may see a hard close or timeout.

Better pattern: wrap the socket in a dedicated connection manager that implements IDisposable, then have MainForm dispose it in FormClosing. Use try-finally or using statements around Receive loops. Also consider: are you handling SocketException on the receive thread? If that thread crashes silently, the UI won't know the connection died.

For a brogue-style game client, this matters more than usual — you'll want heartbeat pings and reconnect logic early.
