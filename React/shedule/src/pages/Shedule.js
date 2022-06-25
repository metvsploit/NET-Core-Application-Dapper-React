import React, {useEffect,  useState} from 'react';
import PostService from "../api/PostService";
import {useFetching} from "../hooks/useFetching";
import Loader from "../UI/Loader";
import PostItem from "../components/PostItem";


function Shedule() {
  const [posts, setPosts] = useState([]);


  const [fetchPost, isPostLoading, postError] = useFetching( async() => {
    const data = await PostService.GetAllShedule();
    setPosts(data);
   });
  
  useEffect(() => {
     fetchPost();
   }, []);

    return (
        <>
        <br/>
        {postError && 
        <h1>Произошла ошибка</h1>}
        {isPostLoading
        ? <Loader/>
        : <PostItem posts={posts}  />
        }
        </>
    );
}

export default Shedule;