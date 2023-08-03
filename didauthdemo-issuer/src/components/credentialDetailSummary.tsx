'use client'

import { useCurrentOwner } from "@/hooks/useCurrentOwner";
import { useCurrentUser } from "@/hooks/useCurrentUser";
import { useEffect, useState } from "react";

export type CredentialDetailSummaryProps = {
    id: string
}

export default function CredentialDetailSummary({id}: CredentialDetailSummaryProps) {
    const [credential, setCredential] = useState({});
    const { currentUser } = useCurrentUser();
    const { token } = useCurrentOwner(currentUser);

    //onload this function fires    
    useEffect(() => {
        if(token === null) return;

        fetch(`http://localhost:5196/api/get-credential-schemas/${token.unique_name}/details/${id}`, { cache: 'no-store'})
        .then(res => {
            if(res.ok) {
                res.json().then(resObj => {
                    setCredential(resObj);
                });
            }
        });
    }, [token]);

    
    return (
        <div>
            <p>{JSON.stringify(credential)}</p>
        </div>
    )
}