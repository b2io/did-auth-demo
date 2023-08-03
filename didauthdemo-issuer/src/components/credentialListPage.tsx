'use client'

import { useEffect, useState } from "react";
import Link from "next/link";
import { useCurrentUser } from "@/hooks/useCurrentUser";
import { useCurrentOwner } from "@/hooks/useCurrentOwner";

function Credential({credential}: any) {
    return (
        <div>
            <h2>{credential.name}</h2>
            <p>{credential.description}</p>
            <Link href={`/credential/${credential.id}`}>
                View
            </Link>
        </div>
    )
}

export default function CredentialListPage() {
    const [credentials, setCredentials] = useState([]);
    const { currentUser } = useCurrentUser();
    const { token } = useCurrentOwner(currentUser);

    useEffect(() => {
        console.log(token);
        if(token === null) return;

        fetch(`http://localhost:5196/api/get-credential-schemas/${token.unique_name}/list`, { cache: 'no-store'})
        .then(res => {
            if(res.ok) {
                res.json().then(resObj => {
                    let credentialList = resObj.map((credential: any) => {
                        return (
                            <Credential key={credential.id} credential={credential} />
                        )
                    });

                    setCredentials(credentialList);
                });
            }
        });
    }, [token]);

    
    return (
        <div>
            <h1>Credentials</h1>
            {credentials && credentials.length > 0 ? <div>{credentials}</div> : <p>No Credentials</p>}
        </div>
    )
}