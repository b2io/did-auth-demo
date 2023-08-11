'use client'

import { useCurrentOwner } from "@/hooks/useCurrentOwner";
import { useCurrentUser } from "@/hooks/useCurrentUser";
import { CredentialDetail } from "@/models";
import { useEffect, useState } from "react";

type CredentialDetailSummaryProps = {
    credential: CredentialDetail
}

export default function CredentialDetailSummary({credential}: CredentialDetailSummaryProps) {

    
    return (
        <div className="grid grid-cols-2 gap-4 card bg-white shadow-lg p-6 my-6">
            <div>
                <h3 className="mb-2 text-md font-bold leading-none tracking-tight">Name</h3>
                <p className="leading-none tracking-tight">{credential.name}</p>
            </div>
            <div>
                <h3 className="mb-2 text-md font-bold leading-none tracking-tight">Created At</h3>
                <p className="leading-none tracking-tight">{credential.createdAt.toLocaleString()}</p>
            </div>
            <div>
                <h3 className="mb-2 text-md font-bold leading-none tracking-tight">Description</h3>
                <p className="leading-none tracking-tight">{credential.description}</p>
            </div>
            <div>
                <h3 className="mb-2 text-md font-bold leading-none tracking-tight">Updated At</h3>
                <p className="leading-none tracking-tight">{credential.updatedAt.toLocaleString()}</p>
            </div>
        </div>
    )
}