
import React from 'react';
import {AppContainer, AppContainerColumn, AppContainerHeader, AppContainerContent} from './style';
import {Container,CssBaseline, TextField} from '@material-ui/core';
import {SimpleSnackbar} from './Message/SimpleSnackBar'

function App() {
  return (
  <>
      <CssBaseline />
      <Container maxWidth="sm">
        <AppContainer>
          <AppContainerColumn>
            <AppContainerHeader>Public Events</AppContainerHeader>
            <AppContainerContent id="publicevents">ssss</AppContainerContent>
          </AppContainerColumn>
          <AppContainerColumn>
            <AppContainerHeader>Private Events</AppContainerHeader>
            <AppContainerContent id="privateevents">pppp</AppContainerContent>
          </AppContainerColumn>
        </AppContainer>
        <AppContainer>
          <AppContainerColumn>
            <input label="Enter your UserName" id="txtUserName"></input>
          </AppContainerColumn>
        </AppContainer>
      </Container>
  </>
  );
}

export default App;
