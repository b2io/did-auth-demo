'use client'

import { useEffect, useState } from "react";
import { useRouter } from 'next/navigation'
import { useCurrentUser } from "@/hooks/useCurrentUser";

export default function AuthGuard({children}: {children: React.ReactNode}) {
    const router = useRouter()
    const {currentUser, isLoadingUser} = useCurrentUser();

    useEffect(() => {
        if(!isLoadingUser && currentUser === null) router.replace('/sign-in');
      }, [isLoadingUser]);

      return (
        <>
            {isLoadingUser ? <></> : children}
        </> 
      )
}