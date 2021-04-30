import React from 'react'
import {List, ListItem, ListItemText, Divider } from '@material-ui/core'
import {Inbox as InboxIcon, Drafts as DraftsIcon} from '@material-ui/icons'
import ListItemIcon from '@material-ui/core/ListItemIcon';

function ListItemLink(props) {
  return <ListItem button component="a" {...props} />;
}

const NotificationList = (notifications) => {
    return (
        <div>
          <List component="nav" aria-label="main mailbox folders">
            
          </List>
        </div>
      );
}

export default NotificationList