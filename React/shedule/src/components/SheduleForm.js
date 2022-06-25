import React from "react";
import PostService from "../api/PostService";
import DatePicker from "react-datepicker";
import "react-datepicker/dist/react-datepicker.css";
import 'bootstrap/dist/css/bootstrap.min.css';
import {  Button, Form } from "react-bootstrap";

class SheduleForm extends React.Component {
    constructor(props) {
        super(props);

        this.state = {directionId: this.props.model.directionId, teacherId: this.props.model.id, 
            date: new Date()};

        this.handleChange = this.handleChange.bind(this);
        this.handleSubmit = this.handleSubmit.bind(this);
    }

    handleChange(date) {
        this.setState({
            date: date,
            directionId: this.props.model.directionId, 
            teacherId: this.props.model.id
        });
    }

    async handleSubmit(event) {
        event.preventDefault();
        const response = await PostService.CreateShedule(this.state);
        if(response.data.data === false){
            alert(response.message);
        }
        else {
            alert("Расписание добавлено");
        }
    }

    render() {
        return(
            <>
             <Form onSubmit={this.handleSubmit}>
               <DatePicker
                selected={this.state.date} onChange={this.handleChange}
                showTimeSelect
                timeFormat="HH:mm:ss"
                timeIntervals={20}
                timeCaption="Время"
                dateFormat="dd.MM.yyyy HH:mm">
               </DatePicker>
               <br/><br/>
               <Button  variant="outline-dark" type="submit">Создать</Button>
           </Form>
            </>
        );
    }
}

export default SheduleForm;