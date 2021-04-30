import {
    HubConnectionBuilder,
    HttpTransportType,
    LogLevel
  } from '@aspnet/signalr'
  
  export const ConnectionState = {
    CONNECTED: 0,
    CONNECTING: 1,
    DISCONNECTED: 2,
    RECONNECTING: 3
  }
  
  const hub = {
    buildConnection(userName) {
      const url = 'http://localhost:5000/hubs/notifications'
      
      const options = {
        transport: HttpTransportType.ServerSentEvents,
        logMessageContent: true,
        logger:
          process.env.NODE_ENV !== 'production' ? LogLevel.Trace : LogLevel.Error
      }
  
      const connection = new HubConnectionBuilder()
        .withUrl(url, options)
        .configureLogging(LogLevel.Error)
        .build()
  
      return connection
    },
    start({ connection, onStart, onError }) {
      connection
        .start()
        .then(() => {
          console.info('SignalR Connected')
          onStart && onStart()
        })
        .catch(err => {
          console.error('SignalR Connection Error: ', err)
          onError && onError(err)
        })
    },
    close(connection) {
      if (!connection) {
        throw new TypeError('Error trying to close connection')
      }
  
      return connection.stop().catch(error => {
        throw new TypeError(error)
      })
    }
  }
  
  export default hub