

import AuthGuard from '@/components/authGuard';
import CredentialDetailSummary from '@/components/credentialDetailSummary';
import React from 'react';

type CredentialDetailPageProps = {
    params: {
        id: string
    }
}

export default function CredentialDetailPage({ params }: CredentialDetailPageProps) {
    return(
        <AuthGuard>
            <div className='container p-6 mx-auto my-3'>
                <h1 className="mb-4 text-3xl font-bold leading-none tracking-tight">Credential Detail</h1>
                <CredentialDetailSummary id={params.id} />
            </div>
        </AuthGuard>
    )
}