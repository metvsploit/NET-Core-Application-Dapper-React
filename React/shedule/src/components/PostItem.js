import React from "react";
import {useNavigate} from 'react-router-dom';
import { Table,Dropdown } from "react-bootstrap";
import SheduleList from "./SheduleList";

const PostItem = function(posts) {
  const router = useNavigate();

  if (posts.posts.length === 0) {
    return (
        <h1 style={{textAlign: 'center'}}>
            Расписание отсутствует
        </h1>
    )
}
    return (
      <>
        <Table striped bordered hover variant="dark">
          <thead>
            <tr>
              <th>
              <Dropdown>
                <Dropdown.Toggle id="dropdown-button-dark-example1" variant="secondary">
                 Направление
                </Dropdown.Toggle>
  
                <Dropdown.Menu variant="dark">
                  <Dropdown.Item  onClick={() => router(`/shedule`)}>Все</Dropdown.Item>
                  <Dropdown.Item  href="/shedule/Бизнес аналитика">Бизнес аналитика</Dropdown.Item>
                  <Dropdown.Item  href="/shedule/Веб-разработчик">Веб-разработчик</Dropdown.Item>
                  <Dropdown.Item  href="/shedule/Инженер по тестированию">Инженер по тестированию</Dropdown.Item>
                  <Dropdown.Item  href="/shedule/Java-разработчик">Java-разработчик</Dropdown.Item>
               </Dropdown.Menu>
              </Dropdown>
              </th>
              <th>Преподаватель</th>
              <th>Дата</th>
              <th>Время</th>
            </tr>
          </thead>
          <tbody>
                 {posts.posts.map((post, index) =>
                      <SheduleList key={post.id} post={post}></SheduleList>
                 )}
          </tbody>
        </Table>
        </>
      );
}

export default PostItem;