import React, {useState} from "react";
import axios from "axios";
import { Formik } from "formik";
import {Button, FormFeedback, FormGroup, Input} from "reactstrap";
import * as Yup from 'yup';
import qs from "qs";
import {useGeneratedId} from "../../../Pages/hooks/useGeneratedId";

export const LeggTilLokasjon = ({ refetch }) => {
    const { generatedId, refetchId } = useGeneratedId()
    return (
        <Formik
            initialValues={{
                Stedsnavn: ''
            }}
            onSubmit={async (values, formikHelpers) => {
                values.StedsNummer = generatedId;
                axios.post('/Lokasjon/RegistrerLokasjon', qs.stringify(values))
                    .then(() => refetch())
                    .catch(e => console.log(e));
                formikHelpers.resetForm();
                refetchId();
            }}
            validationSchema={Yup.object().shape({
                Stedsnavn: Yup.string().required('Fyll ut lokasjonsnavn'),
            })}
        >
            {props => {
                const {
                    values,
                    touched,
                    errors,
                    isSubmitting,
                    handleChange,
                    handleBlur,
                    handleSubmit,
                } = props;

                return (
                    <form onSubmit={handleSubmit}>
                        <FormGroup>
                            <label>Legg til lokasjon</label>
                            <Input
                                id={'Stedsnavn'}
                                type={'text'}
                                value={values.Stedsnavn}
                                onChange={handleChange}
                                onBlur={handleBlur}
                                invalid={errors.Stedsnavn && touched.Stedsnavn}
                            />
                            <FormFeedback>{errors.Stedsnavn}</FormFeedback>
                        </FormGroup>
                        <Button
                            className={'btn btn-success'}
                            disabled={isSubmitting}
                        >
                            Legg til lokasjon
                        </Button>
                    </form>
                );
            }}
        </Formik>
    )
}