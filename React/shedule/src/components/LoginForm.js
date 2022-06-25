import React from "react";
import { Accordion, Button, Form } from "react-bootstrap";
import PostService from "../api/PostService";

class LoginForm extends React.Component{
    constructor(props) {
        super(props);
        this.state = {email: '', password: ''};
          
        this.handleChange = this.handleChange.bind(this);
        this.handleSubmit = this.handleSubmit.bind(this);
      }

      handleChange(event) {
        const target = event.target;
        const value = target.value;
        const name = target.name;
        this.setState({[name]:value});
      }
    
      async handleSubmit(event) {
    
        event.preventDefault();
        const response = await PostService.Login(this.state);
        if(response.data[0] === "token") {
          alert(response.message)
        }
        else {
          console.log(response);
          localStorage.setItem("Bearer", response.data[0]);
          localStorage.setItem("Role", response.data[1]);
          window.location.href = "http://localhost:3000/shedule";
        }
      }

render() {
    return(
      
        <Accordion defaultActiveKey="0">
        <Accordion.Item eventKey="1">
          <Accordion.Header>Вход</Accordion.Header>
          <Accordion.Body>
          <Form onSubmit={this.handleSubmit}>
    <Form.Group className="mb-2" controlId="formBasicEmail">
      <Form.Label variant="light" >Email адрес</Form.Label>
      <Form.Control name="email" style={{width:'200px'}} type="email" placeholder="Enter email"
      value={this.state.email} onChange={this.handleChange}/> 
    </Form.Group>
  
    <Form.Group className="mb-2" controlId="formBasicPassword">
      <Form.Label>Пароль</Form.Label>
      <Form.Control name="password" style={{width:'200px'}} type="password" placeholder="Password"
       value={this.state.password} onChange={this.handleChange}/> 
    </Form.Group>
  
    <Button  variant="outline-dark" type="submit"  >
      Войти
    </Button>
  </Form>
          </Accordion.Body>
        </Accordion.Item>
        </Accordion>
    );
}
   
}

export default LoginForm;
