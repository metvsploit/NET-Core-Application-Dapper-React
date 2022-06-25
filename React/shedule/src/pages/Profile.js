import React, {useEffect,  useState} from 'react';
import PostService from '../api/PostService';
import { useFetching } from '../hooks/useFetching';
import { Alert, Col,Row } from 'react-bootstrap';
import SheduleForm from '../components/SheduleForm';
import SheduleTable from '../components/teacher/SheduleTable';
import Loader from '../UI/Loader';

const Profile = () => {
    const[data, setData] = useState([]);
    const[table, setTable] = useState([]);

    const [fetchPost, isPostLoading] = useFetching(async() => {
        const response = await PostService.GetUserData();
        setData(response.data.data);
        const shedule = await PostService.GetSheduleByTeacher(response.data.data.id);
        setTable(shedule);
    });

    useEffect(() => {
        fetchPost();
       }, []);

       if(isPostLoading){
        return <Loader/>
    }
       
    return(
        <div>
            <Row>
              <Col sm-2>
                <Alert style={{'width':'300px','margin': '50px'}} variant="primary">
                  <Alert.Heading>Личные данные</Alert.Heading>
                    <hr />
                    <h5>Имя Фамилия:</h5>
                    {data.lastName} {data.firstName}
                    <h5>Направление:</h5>
                    {data.directionName}
                    </Alert>
              </Col>
            
            {localStorage.getItem("Role") === 'Teacher'?(
                <Col>
                 <Alert style={{'width':'300px','margin': '50px'}} variant="primary">
                 <Alert.Heading>Добавить расписание</Alert.Heading>
                   <hr />
                   <h5>Выберите дату и время:</h5>
                   <SheduleForm model={data}></SheduleForm>
                </Alert>
                </Col>
                
            ):null
      }
            </Row>
             <SheduleTable posts={table}></SheduleTable>
        </div>
    );
}

export default Profile;