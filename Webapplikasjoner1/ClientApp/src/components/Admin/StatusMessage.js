import React from "react";

export const StatusMessage = ({ status }) => {
    if (!status) {
        return null;
    }
    return (
        <p className={'mb-2 text-danger'}>{status}</p>
    )
}