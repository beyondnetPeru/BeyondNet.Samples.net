import { Button, Card, CardActions, CardContent, CardHeader, Menu, Typography } from "@material-ui/core"
import NotificationList from './NotificationList'

const NotifyLayout = ({anchorEl, isMenuOpen, handleMenuClose}) => {
    return (
        <Menu
            anchorEl={anchorEl}
            anchorOrigin={{ vertical: 'top', horizontal: 'right' }}
            id='menu-bar-app'
            keepMounted
            transformOrigin={{ vertical: 'top', horizontal: 'right' }}
            open={isMenuOpen}
            onClose={handleMenuClose}
            >
            <Card> 
                <CardHeader>
                <Typography>
                        Notificatons
                    </Typography>
                </CardHeader>
                <CardContent>
                    <NotificationList />
                </CardContent>
                <CardActions>
                    <Button>Close</Button>
                </CardActions>
            </Card>
        </Menu>
    );
}

export default NotifyLayout