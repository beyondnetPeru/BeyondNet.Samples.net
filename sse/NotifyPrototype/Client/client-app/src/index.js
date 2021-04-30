import React from 'react';
import ReactDOM from 'react-dom';
import App from './App.jsx'
import { hub } from './Services'

function ConnectToHub() {
  const connection = hub.buildConnection('beyondnet')
  
  connection.on('PublicNotificationPush', message => {
    let object = JSON.parse(message)

    if (object.NotificationType === 'pub') {
      let paragraph = document.getElementById("publicevents");
      
      let text = document.createTextNode(message);
      
      paragraph.appendChild(text);
      
      console.debug(message);
    }
  })
  
  connection.on('PrivateNotificationPush', message => {
    let object = JSON.parse(message)
    let user = document.getElementById("txtUserName").value;

    if (object.NotificationType === 'prv' && object.User.toUpperCase() === user.toUpperCase()) {
      let paragraph = document.getElementById("privateevents");
      
      let text = document.createTextNode(message);
      
      paragraph.appendChild(text);
      
      console.debug(message);
    }
  })
  

  return connection;
}

 ConnectToHub().start();

ReactDOM.render(
  <React.StrictMode>
    <App />
  </React.StrictMode>,
  document.getElementById('root')
);

