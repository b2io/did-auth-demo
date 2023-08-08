export type CredentialDetail = {
    id: number,
    name: string,
    description: string,
    ownerDid: string,
    schemaDefinition: CredentialClaimSchema[],
    createdAt: Date,
    updatedAt: Date
}

export type CredentialClaimSchema = {
    label: string,
    type: string,
    claim: string
}