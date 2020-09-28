import React, { useEffect } from "react";
import PropTypes from "prop-types";
import { Field, reduxForm } from "redux-form";
import { Form, Button } from "reactstrap";
import Validator from "../../../../common/helpers/YupValidator";
import InputField from "../../../../components/inputs/InputField";
import SelectField from "../../../../components/inputs/SelectField";
import schema from "../validation";
import { getAll } from "../../../productType/list/"
import { connect } from "react-redux";

const ProductForm = props => {

  const { handleSubmit, handleCancel, productTypeList } = props;
  
  useEffect(function () {
    props.getAll()
  }, [])

  return (
    <Form onSubmit={handleSubmit}>
      <Field
        label="Nombre"
        name="name"
        component={InputField}
        type="text" />
      <Field
        label="Precio de Compra"
        name="costPrice"
        component={InputField}
        type="text"
      />
      <Field
        label="Precio de Venta"
        name="salePrice"
        component={InputField}
        type="text"
      />
      <Field
        label="modificar Stock"
        name="stock"
        component={InputField}
        type="text"
      />
      <Field
        label="Product Type"
        name="ProductTypeId"
        component={SelectField}
        options={productTypeList.map(pt => ({value: pt.id, label: pt.description}))}
        type="select"
      />
      <Button className="ProductType-form__button" color="primary" type="submit">
        Guardar
      </Button>
      <Button
        className="ProductType-form__button"
        color="secondary"
        type="button"
        onClick={handleCancel}
      >
        Cancelar
      </Button>
    </Form>
  );
};

const mapStateToProps = function (state) {
  return {
    productTypeList: state.productType.list.productTypes
  }
}

const ConnectedForm = connect(mapStateToProps, { getAll })(ProductForm)

ProductForm.propTypes = {
  handleSubmit: PropTypes.func.isRequired,
  handleCancel: PropTypes.func.isRequired,
  productTypeList: PropTypes.array.isRequired
};


export default reduxForm({
  form: "product",
  validate: Validator(schema),
  enableReinitialize: true
})(ConnectedForm);
