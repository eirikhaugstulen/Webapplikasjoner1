import React from "react";
import axios from "axios";
import { Formik } from "formik";
import {Button, FormFeedback, FormGroup, Input} from "reactstrap";
import * as Yup from 'yup';
import qs from "qs";
import {useGeneratedId} from "../../../Pages/hooks/useGeneratedId";
import {checkUnauthorized} from "../../../utils/checkUnauthorized";
import {StatusMessage} from "../StatusMessage";

export const LeggTilLokasjon = ({ refetch }) => {
    const { generatedId, refetchId } = useGeneratedId()
    return (
        <Formik
            initialValues={{
                Stedsnavn: ''
            }}
            onSubmit={async (values, formikHelpers) => {
                values.StedsNummer = generatedId;
                axios.post('/Lokasjon/Registrer', qs.stringify(values))
                    .then(() => refetch())
                    .catch(e => {
                        console.log(e)
                        formikHelpers.setStatus('Det har skjedd en feil! Se console for mer.');
                        checkUnauthorized(e)
                    });
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
                    status,
                    isSubmitting,
                    handleChange,
                    handleBlur,
                    handleSubmit,
                } = props;

                return (
                    <form onSubmit={handleSubmit}>
                        <StatusMessage status={status} />
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