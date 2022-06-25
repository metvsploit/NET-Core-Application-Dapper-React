import React, { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import PostService from "../api/PostService";
import { useFetching } from "../hooks/useFetching";
import Loader from "../UI/Loader";
import PostItem from "../components/PostItem";


const SheduleDirection = () => {
    const params = useParams();
    
    const [posts, setPosts] = useState([]);
    const [fetchPostByName, isPostLoading, postError] = useFetching( async(name) => {
       
      const data = await PostService.GetSheduleByName(name);
      setPosts(data);
     });
    
      useEffect(() => {
       fetchPostByName(params.name);
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

export default SheduleDirection;