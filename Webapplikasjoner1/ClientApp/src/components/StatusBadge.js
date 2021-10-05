import React from "react";
import {Badge} from "reactstrap";

export const StatusBadge = ({ active }) => (
    <>
        {active ? (
            <td>
                <Badge color="success" pill>Aktiv</Badge>
            </td>
        ) : (
            <td>
                <Badge color="secondary" pill>Utgått</Badge>
            </td>
        )}
    </>
)