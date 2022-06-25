import React from "react";
import { Button,Table } from "react-bootstrap";
import PostService from "../../api/PostService";

const SheduleTable = (posts) => {

    async function Delete(id) {
        const response = await PostService.DeleteSheduleById(id);
        if(response.status == 401) {
            alert("Данное действие вам недоступно");
        }
        else {
            alert(response);
        }
    }

    return(
        <>
        <Table striped bordered hover variant="dark">
          <thead>
            <tr>
              <th>Направление</th>
              <th>Преподаватель</th>
              <th>Дата</th>
              <th>Время</th>
              <th>#</th>
            </tr>
          </thead>
          <tbody>
           
                 {posts.posts.map((post, index) =>
                    <tr>
                    <td>{post.directionName}</td>
                    <td>{post.teacherName}</td>
                    <td>{post.dateTime.split('T')[0]}</td>
                    <td>{post.dateTime.split('T')[1]}</td>
                    <td><Button variant="danger" onClick={() => Delete(post.id)}>Удалить</Button></td>
                </tr>
                 )}
          </tbody>
        </Table>
        </>
    );
}

export default SheduleTable;