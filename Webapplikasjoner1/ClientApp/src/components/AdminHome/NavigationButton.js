import React, {useState} from "react";
import {Button} from "reactstrap";
import history from "../../history";

const pushToPage = (page) => history.push(`/admin/${page}`);

export const NavigationButton = ({ tittel, tekst, page }) => (
        <div
            className={'d-flex justify-content-center h-100 rounded p-4 shadow-sm'}
            style={{backgroundColor: '#E5E7EB'}}
        >
            <div className={'text-center d-flex flex-column justify-content-between'}>
                <h5 className={'flex-none'}>{tittel}</h5>
                <p>
                    {tekst}
                </p>
                {page && (
                    <Button 
                        onClick={() => pushToPage(page)}
                    >
                        Administrer
                    </Button>
                )}
            </div>
        </div>
    )