import React from "react";

const SheduleList = function(props) {
    return (
    <tr>
        <td>{props.post.directionName}</td>
        <td>{props.post.teacherName}</td>
        <td>{props.post.dateTime.split('T')[0]}</td>
        <td>{props.post.dateTime.split('T')[1]}</td>
    </tr>
    );

}

export default SheduleList;