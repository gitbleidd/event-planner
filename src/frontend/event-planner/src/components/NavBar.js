import { useState } from "react";
import { NavLink } from "react-router-dom";
import Button from 'react-bootstrap/Button';
import Container from 'react-bootstrap/Container';
import Nav from 'react-bootstrap/Nav';
import Navbar from 'react-bootstrap/Navbar';
import AuthorizationModal from "./AuthorizationModal";
import 'bootstrap/dist/css/bootstrap.min.css';
import "./NavBar.css";
import "./SecondaryRoundedButton.css";
import * as routes from "../shared/routes"

function NavBar() {
    const [authorizationModalShow, setAuthorizationModalShow] = useState(false);
    return (
        <Navbar bg="white" expand="sm">
            <Container fluid>
                <Navbar.Toggle aria-controls="navbarScroll" />
                <Navbar.Collapse id="navbarScroll">
                    <Nav
                        defaultActiveKey="/"
                        className="me-auto my-2 my-lg-0"
                        style={{ maxHeight: '100px' }}
                    >
                        <Nav.Link as={NavLink} to={routes.homeRoute}>Домашняя страница</Nav.Link>
                        <Nav.Link as={NavLink} to={routes.newEventRoute}>Создать мероприятие</Nav.Link>
                    </Nav>
                    <Button variant="secondary-rounded" onClick={() => setAuthorizationModalShow(true)}>
                        Войти
                    </Button>
                    <AuthorizationModal 
                        show={authorizationModalShow}
                        onHide={() => setAuthorizationModalShow(false)} />
                </Navbar.Collapse>
            </Container>
        </Navbar>
    );
}
export default NavBar;