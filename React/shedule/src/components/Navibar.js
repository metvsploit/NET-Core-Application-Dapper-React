import React, { useContext } from "react";
import {Nav,Container,Navbar,NavDropdown} from "react-bootstrap";
import {useNavigate} from 'react-router-dom';
import { AuthContext } from "../context";
import LoginForm from "./LoginForm";

const Navibar = () => {
  const router = useNavigate();
  const {isAuth, setIsAuth} = useContext(AuthContext);
  console.log(isAuth);

  const logout = () => {
    localStorage.removeItem("Bearer");
    localStorage.removeItem("Role")
  }

    return (
        <Navbar bg="dark" variant="dark" expand="lg">
      <Container>
        <Navbar.Brand href="#home">React-Bootstrap</Navbar.Brand>
        <Navbar.Toggle aria-controls="basic-navbar-nav" />
        <Navbar.Collapse id="basic-navbar-nav">
          <Nav className="me-auto">
        
          <Nav.Link href="/shedule">Расписание</Nav.Link>
          </Nav>

             {isAuth ? (
              <Nav >
                     <NavDropdown title="Аккаунт" id="basic-nav-dropdown">
                     <NavDropdown.Item href="/account/profile">Мой профиль</NavDropdown.Item>
            
                     <NavDropdown.Divider />
                     <NavDropdown.Item href="/shedule" onClick= {logout}>
                       Выход
                      </NavDropdown.Item>
                   </NavDropdown>
                   </Nav>
    )
         
          :(
            <Nav>
            <LoginForm></LoginForm>
            </Nav>
          )}
          
        </Navbar.Collapse>
      </Container>
    </Navbar>

    );
}


export default Navibar;